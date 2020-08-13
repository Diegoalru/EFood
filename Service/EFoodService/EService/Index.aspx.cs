using System;
using System.Web.UI;

namespace EService
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) {}
        
        protected void Btn_Tarjeta_Click(object sender, EventArgs e) => Response.Redirect("~/TarjetaView.aspx");
        public void Btn_Cheque_Click(object sender, EventArgs e) => Response.Redirect("~/Cheque.aspx");
        
    }
}