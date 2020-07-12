$("body").on("change", "#ddl", function () {
    $("#hf").val($(this).find("option:selected").text());
});

