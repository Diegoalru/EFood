/*
 BASE DE DATOS PARA EFOOD-CLIENTE

 @AUTOR DIEGO ALONSO RUBI SALAS
 @DATE 20200808
 */

/*
USE MASTER;
GO

IF EXISTS(SELECT NAME FROM sys.databases WHERE NAME LIKE N'EFoodCliente')
BEGIN
    EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'EFoodCliente';
    ALTER DATABASE EFoodCliente SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE EFoodCliente;
END
GO

IF NOT EXISTS(SELECT NAME FROM sys.databases WHERE NAME LIKE N'EFoodCliente')
BEGIN
    CREATE DATABASE EFoodCliente COLLATE Modern_Spanish_BIN2;
END
GO
*/

/*
* Para restablecer las llaves primarias
* DBCC CHECKIDENT (<Tabla>, RESEED, 0);
*/

USE EFoodCliente;
GO

DROP TABLE IF EXISTS ERROR;
DROP TABLE IF EXISTS TARJETA;
DROP TABLE IF EXISTS CHEQUE;
DROP TABLE IF EXISTS CUENTA;

CREATE TABLE TARJETA
(
    CODE INTEGER IDENTITY (1, 1)
    ,NOMBRE_ASOCIADO VARCHAR(30)
    ,TARJETA VARCHAR(16)
    ,MES NVARCHAR(2)
    ,YEAR NVARCHAR(4)
    ,CVV NVARCHAR(4)
    ,TIPO INTEGER
    ,SALDO DECIMAL(8, 2)
    ,CONSTRAINT PK_TARJETA_CODE PRIMARY KEY (CODE)
    ,CONSTRAINT NN_TARJETA_NOMBRE CHECK (NOMBRE_ASOCIADO IS NOT NULL )
    ,CONSTRAINT NN_TARJETA_TARJETA CHECK (TARJETA IS NOT NULL )
    ,CONSTRAINT CK_TARJETA_TARJETA CHECK (LEN(TARJETA) = 16)
    ,CONSTRAINT NN_TARJETA_MES CHECK (MES IS NOT NULL)
    ,CONSTRAINT CK_TARJETA_MES CHECK (LEN(MES) = 2)
    ,CONSTRAINT NN_TARJETA_YEAR CHECK (YEAR IS NOT NULL )
    ,CONSTRAINT CK_TARJETA_YEAR CHECK (LEN(YEAR) = 4)
    ,CONSTRAINT NN_TARJETA_CVV CHECK (CVV IS NOT NULL )
    ,CONSTRAINT CK_TARJETA_CVV CHECK (LEN(CVV) <= 4)
    ,CONSTRAINT NN_TARJETA_TIPO CHECK (TIPO IS NOT NULL )
    ,CONSTRAINT NN_TARJETA_SALDO CHECK (SALDO IS NOT NULL)
    ,CONSTRAINT CK_TARJETA_SALDOINCORRECTO CHECK (SALDO > 0)    
);

DROP INDEX IF EXISTS TARJETA.IDX_TARJETA_TARJETA;
CREATE INDEX  IDX_TARJETA_TARJETA ON TARJETA ( TARJETA );

CREATE TABLE CUENTA
(
    CODE INT IDENTITY(1, 1)
    ,CUENTA INT
    ,SALDO DECIMAL(8,2)
    ,CONSTRAINT PK_CUENTA_CODE PRIMARY KEY ( CODE )
    ,CONSTRAINT NN_CUENTA_CUENTA CHECK ( CUENTA IS NOT NULL )
    ,CONSTRAINT UQ_CUENTA_CUENTA UNIQUE (CUENTA)
    ,CONSTRAINT NN_CUENTA_SALDO CHECK ( SALDO IS NOT NULL )
    ,CONSTRAINT CK_CUENTA_SALDO CHECK ( SALDO >= 0)   
);

DROP INDEX IF EXISTS CUENTA.IDX_CUENTA_CUENTA;
CREATE INDEX  IDX_CUENTA_CUENTA ON CUENTA ( CUENTA );  

CREATE TABLE CHEQUE
(
    CODE INT IDENTITY(1, 1)
    ,CUENTA INT
    ,NUMERO VARCHAR(8)
    ,CONSTRAINT PK_CHEQUE_CODE PRIMARY KEY ( CODE )
    ,CONSTRAINT NN_CHEQUE_CUENTA CHECK ( CUENTA IS NOT NULL )
    ,CONSTRAINT FK_CHEQUE_CUENTA FOREIGN KEY (CUENTA) REFERENCES CUENTA ( CUENTA )
    ,CONSTRAINT NN_CHEQUE_NUMERO CHECK( NUMERO IS NOT NULL )
    ,CONSTRAINT UQ_CHEQUE_DATOSDUPLICADOS UNIQUE ( CUENTA, NUMERO )
);

CREATE TABLE ERROR
(
    CODE INTEGER IDENTITY (1, 1)
    ,FECHA DATETIME DEFAULT GETDATE()
    ,ERROR INTEGER
    ,MENSAJE NVARCHAR(200)
    ,CONSTRAINT PK_ERROR_CODE PRIMARY KEY (CODE)
    ,CONSTRAINT NN_ERROR_ERROR CHECK (ERROR IS NOT NULL )
    ,CONSTRAINT NN_ERROR_MENSAJE CHECK (MENSAJE IS NOT NULL )
);

USE efoodcliente;
GO

/*
 * CHEQUE
 */
 
CREATE OR ALTER FUNCTION dbo.EXISTE_CUENTA
( @CUENTA INT )
    RETURNS BIT
AS
BEGIN
    DECLARE @RESULTADO BIT = 0;
    IF EXISTS(SELECT * FROM CUENTA WHERE CUENTA = @CUENTA)
        BEGIN
            SET  @RESULTADO = 1;
        END
    RETURN @RESULTADO;
END
GO

CREATE OR ALTER FUNCTION dbo.EXISTE_NUMERO_CHEQUE
(
    @CUENTA INT
    ,@NUMERO VARCHAR(8)
)
RETURNS BIT 
AS
    BEGIN
        DECLARE @RESULT BIT = 0
        IF EXISTS(SELECT * FROM CHEQUE WHERE CUENTA = @CUENTA AND NUMERO = UPPER(@NUMERO))
        BEGIN
           SET @RESULT = 1;
        END
        RETURN @RESULT;
    END
GO

CREATE OR ALTER PROCEDURE ACTUALIZA_SALDO_CUENTA
(
    @CUENTA INT
    ,@MONTO DECIMAL(8,2)
)
AS
BEGIN
    UPDATE CUENTA SET SALDO = SALDO - @MONTO WHERE CUENTA = @CUENTA;  
END
GO

CREATE OR ALTER FUNCTION VALIDA_REBAJO_CUENTA
(
    @CUENTA INT
    ,@MONTO DECIMAL(8, 2)
)
RETURNS BIT
AS 
BEGIN
    DECLARE @RESULT BIT;
    DECLARE @DISPONIBLE DECIMAL(8,2);
    SELECT  @DISPONIBLE = (SALDO - @MONTO) FROM CUENTA WHERE CUENTA = @CUENTA;
    IF(@DISPONIBLE >= 0)
    BEGIN
       SET @RESULT = 1;
    END
    ELSE
    BEGIN
        -- ERROR: No hay fondos suficientes.
       SET @RESULT = 0;
    END
    RETURN @RESULT;
END
GO

CREATE OR ALTER PROCEDURE dbo.INSERTA_CUENTA_CHEQUE
(
    @CUENTA INT
    ,@SALDO DECIMAL(8, 2)
)
AS
BEGIN
    INSERT
    INTO
        CUENTA
    (CUENTA, SALDO)
    VALUES
    (@CUENTA, @SALDO);
END
GO

CREATE OR ALTER PROCEDURE INSERTA_CHEQUE
(
    @CUENTA INT
,@NUMERO VARCHAR(8)
)
AS
BEGIN
    INSERT INTO CHEQUE (CUENTA, NUMERO) VALUES (@CUENTA, @NUMERO);
END
GO

CREATE OR ALTER FUNCTION VALIDA_NUMERO_CHEQUE
(
    @CUENTA INT
    ,@NUMERO VARCHAR(8)
)
    RETURNS INT
AS
BEGIN
    DECLARE @RESULT INT
    IF(dbo.EXISTE_CUENTA(@CUENTA) = 1)
        BEGIN
            IF(dbo.EXISTE_NUMERO_CHEQUE(@CUENTA, @NUMERO) = 1)
                BEGIN
                    -- ERROR: Cheque duplicado.
                    SET @RESULT = -1;
                END
            ELSE
                BEGIN
                    -- Transaccion exitosa.
                    SET @RESULT = 0;
                END
        END
    ELSE
        BEGIN
            -- No existe cuenta.
            SET @RESULT = -2;
        END
    RETURN @RESULT;
END
GO

/*
 * TARJETAS
 */

CREATE OR ALTER FUNCTION dbo.EXISTE_TARJETA
( 
    @TARJETA VARCHAR(16) 
)
RETURNS BIT
AS
BEGIN
    DECLARE @RESULTADO BIT = 0;
    IF EXISTS(SELECT * FROM TARJETA WHERE TARJETA = @TARJETA)
        BEGIN
            SET  @RESULTADO = 1;
        END
    RETURN @RESULTADO;
END
GO

CREATE OR ALTER PROCEDURE ACTUALIZA_SALDO_TARJETA
(
    @TARJETA VARCHAR(16), @MONTO DECIMAL(8,2)
)
AS
BEGIN
    UPDATE TARJETA SET SALDO = SALDO - @MONTO WHERE TARJETA = @TARJETA;
END
GO

CREATE OR ALTER FUNCTION VALIDA_REBAJO_TARJETA
(
    @TARJETA VARCHAR(16)
    ,@MONTO DECIMAL(8, 2)
)
    RETURNS BIT
AS
BEGIN
    DECLARE @RESULT BIT;
    DECLARE @DISPONIBLE DECIMAL(8,2);
    SELECT  @DISPONIBLE = (SALDO - @MONTO) FROM TARJETA WHERE TARJETA = @TARJETA;
    IF(@DISPONIBLE >= 0)
        BEGIN
            SET @RESULT = 1;
        END
    ELSE
        BEGIN
            -- ERROR: No hay fondos suficientes.
            SET @RESULT = 0;
        END
    RETURN @RESULT;
END
GO

CREATE OR ALTER FUNCTION VALIDA_TARJETA
(
    @TARJETA VARCHAR(16)
    ,@MES NVARCHAR(2)
    ,@YEAR NVARCHAR(4)
    ,@CVV NVARCHAR(4)
)
RETURNS INT
AS
BEGIN
    DECLARE @RESULT INT;
    IF EXISTS(SELECT * FROM TARJETA WHERE TARJETA = @TARJETA)
    BEGIN
        IF EXISTS(SELECT * FROM TARJETA WHERE TARJETA = @TARJETA AND MES = @MES AND YEAR = @YEAR)
        BEGIN
            IF EXISTS(SELECT * FROM TARJETA WHERE TARJETA = @TARJETA AND CVV = @CVV)
            BEGIN
                SET @RESULT = 0;
            END
            ELSE
            BEGIN
               SET @RESULT = -3;  
            END
        END
        ELSE
        BEGIN
           SET @RESULT = -2; 
        END
    END
    ELSE
    BEGIN
        SET @RESULT = -1;
    END
    RETURN @RESULT;
END
GO

CREATE OR ALTER PROCEDURE dbo.INSERTA_TARJETA
(
    @NOMBRE_ASOCIADO VARCHAR(30)
    ,@TARJETA VARCHAR(16)
    ,@MES NVARCHAR(2)
    ,@YEAR NVARCHAR(4)
    ,@CVV NVARCHAR(4)
    ,@TIPO INTEGER
    ,@SALDO DECIMAL(8, 2)
)
AS
BEGIN
    INSERT
        INTO
            TARJETA
                (NOMBRE_ASOCIADO, TARJETA, MES, YEAR, CVV, TIPO, SALDO)
            VALUES
                (UPPER(@NOMBRE_ASOCIADO), @TARJETA, @MES, @YEAR, @CVV, @TIPO, @SALDO);
END
GO
