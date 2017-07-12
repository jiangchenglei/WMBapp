using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bill.Utility.Helper
{
    public class EmailBuilder
    {
        /// <summary>
        /// 商机审核通过模板
        /// </summary>
        /// <param name="Email">用户名</param>
        /// <param name="OpportunityDesc">商机描述</param>
        /// <returns></returns>
        public static string BuildOpportunityCheckPass(string UserName, string OpportunityDesc)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<style type=\"text/css\">");
            sb.AppendLine(".content{margin-left: auto;margin-right: auto;width: 800px;margin-top: 10px;line-height:120%;}");
            sb.AppendLine(".content-line{overflow: hidden;width: 100%;}");
            sb.AppendLine(".box{overflow: hidden;margin-left: auto;margin-right: auto;border-right: 1px solid #F0F6FA;border-bottom: 1px solid #F0F6FA;padding: 0 1px 1px 0;background-color: #E0ECF5;color: #4f4e4e;	padding-bottom: 1px;padding-right:1px;}");
            sb.AppendLine(".boxbg{background-color: #F9FCFD;border: 1px solid #BDC8D9;padding-left:20px;padding-right:20px;padding-top:10px;padding-bottom:20px;}");
            sb.AppendLine("</style>");
            sb.AppendLine("<div class=\"content\"><div class=\"content-line\"><div class=\"box\"><div class=\"boxbg\">");
            sb.AppendLine("<p>Theme: Post your Buying Lead successfully!</p>");
            sb.AppendLine("<p>Welcome to Osell.com!</p>");
            sb.AppendLine("<p>You post your buying leads about “" + OpportunityDesc + "” successfully, and you will receive quotations from up to 10 different Chinese suppliers within 30 days.</p>");
            sb.AppendLine("<p>To help you better source from China on Osell.com, you are warmly welcome to search more information on <a target=\"_blank\" href=\"http://www.osell.com\">OSELL</a> , where you can find free samples and talk to suppliers with free instant translation.</p>");
            sb.AppendLine("<p>Dear " + UserName + "</p>");
            sb.AppendLine("<p>Links to download Osell:</p>");
            sb.AppendLine("<p>Android:<br />IOS:<br /></p>");
            sb.AppendLine("<p>If you have any question, please contact us.</p>");
            sb.AppendLine("<p>Wishing you the very best of business,</p>");
            sb.AppendLine("<p><a target=\"_blank\" href=\"http://www.osell.com\">Osell.com</a>  Service Team</p>");
            sb.AppendLine("</div></div></div></div>");
            return sb.ToString();
        }


        public static string BuildOpportunityReplySendEmail(string UserName, string OpportunityDesc, string CompanyName, string CompanyEmail, string productJson = "")
        {
            List<OpportunityProductInModel> productInfo = JsonHelper.Deserializer<List<OpportunityProductInModel>>(productJson);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<style type=\"text/css\">");
            sb.AppendLine(".content{margin-left: auto;margin-right: auto;width: 800px;margin-top: 10px;line-height:120%;}");
            sb.AppendLine(".content-line{overflow: hidden;width: 100%;}");
            sb.AppendLine(".box{overflow: hidden;margin-left: auto;margin-right: auto;border-right: 1px solid #F0F6FA;border-bottom: 1px solid #F0F6FA;padding: 0 1px 1px 0;background-color: #E0ECF5;color: #4f4e4e;	padding-bottom: 1px;padding-right:1px;}");
            sb.AppendLine(".boxbg{background-color: #F9FCFD;border: 1px solid #BDC8D9;padding-left:20px;padding-right:20px;padding-top:10px;padding-bottom:20px;}");
            sb.AppendLine(".containertable {width: 100%;border-right: 1px solid #d8d8d8;border-bottom: 1px solid #d8d8d8;}");
            sb.AppendLine(".containertable td, .containertable th{padding: 5px 0;line-height: 30px;border-left: 1px solid #d8d8d8;border-top: 1px solid #d8d8d8;text-align: center;}");
            sb.AppendLine("</style>");
            sb.AppendLine("<div class=\"content\"><div class=\"content-line\"><div class=\"box\"><div class=\"boxbg\">");
            sb.AppendLine("<p>Theme: You have a new reply from Chinese Supplier at Osell.com <br/>Dear " + UserName + ",</p>");
            sb.AppendLine("<p>Your buying lead about “" + OpportunityDesc + "” is replied by one Chinese supplier. Please check the details below :</p>");

            if (productInfo != null && productInfo.Count > 0)
            {
                sb.AppendLine("<table class=\"containertable\"><tbody><tr style=\"height: 60px\"><td style=\"background-color: #eee;\" colspan=\"7\">Supplier Profile</td></tr>");
                sb.AppendLine("<tr><th width=\"13\">Company Name</th><th width=\"13\">Product Name</th><th width=\"13\">Product Image</th><th width=\"13\">Product Describtion</th><th width=\"13\">Pieces</th><th width=\"13\">Price</th><th width=\"13\">Email</th></tr>");
                int rows = 1;
                foreach (var item in productInfo)
                {

                    sb.AppendLine("<tr>");
                    if (rows == 1)
                    {
                        sb.AppendLine("<td rowspan=" + productInfo.Count + " style=\"line-height: 22px\">" + CompanyName + "</td>");
                    }
                    sb.AppendLine("<td style=\"text-align: left;\">" + item.ProductName + "</td><td>");
                    if (!string.IsNullOrWhiteSpace(item.ProductImg))
                    {
                        foreach (var imgPath in item.ProductImg.Split(','))
                        {
                            sb.AppendLine("<li><div><a href=" + imgPath + " target=\"_blank\"><img class=\"img_show\" width=\"80\" src=" + imgPath + "></a></div></li>");
                        }
                    }
                    sb.AppendLine("</td><td>" + item.ProductParam + "</td><td>" + item.ProductCount + "</td><td>" + (string.IsNullOrEmpty(item.Currency) ? "$" : item.Currency == "USD" ? "$" : "") + "" + item.PriceRange + "</td>");
                    if (rows == 1)
                    {
                        sb.AppendLine("<td rowspan=" + productInfo.Count + ">" + CompanyEmail + "</td>");
                    }
                    sb.AppendLine("</tr>");
                    //else
                    //{
                    //    sb.AppendLine("<tr><td style=\"text-align: left;\">" + item.ProductName + "</td><td>");
                    //    if (!string.IsNullOrWhiteSpace(item.ProductImg))
                    //    {
                    //        foreach (var imgPath in item.ProductImg.Split(','))
                    //        {
                    //            sb.AppendLine("<li><div><a href=" + imgPath + " target=\"_blank\"><img class=\"img_show\" width=\"80\" src=" + imgPath + "></a></div></li>");
                    //        }
                    //    }
                    //    <img width=\"100\" src='" + item.ProductImg + "'></td><td>" + item.ProductParam + "</td><td>" + item.ProductCount + "</td><td>" + (string.IsNullOrEmpty(item.Currency) ? "$" : item.Currency == "USD" ? "$" : "") + "" + ConvertHelper.ToDecimal(item.PriceRange) + "</td></tr>");
                    //}
                    rows++;
                }
                sb.AppendLine("</tbody></table>");
            }

            sb.AppendLine("<p>You are warmly recommended to contact the suppliers on OSELL APP, where you can find more free samples and talk to suppliers with free instant translation.</p>");

            sb.AppendLine("<p>Links to download <a target=\"_blank\" href=\"http://oc.osell.com/app/Download_en\">Osell</a><br /></p>");
            sb.AppendLine("<p><img height=\"160\" width=\"160\" src=\"http://www.18985.com/content/images/osellqrcode.jpg\" /></p>");
            sb.AppendLine("<p>You could also contact the suppliers through the contact info above.</p>");
            sb.AppendLine("<pIf you have any question, please <a href=\"mailto:BuyerService@osell.com\" target=\"_blank\">contact us</a>.</p>");
            sb.AppendLine("<p>Wishing you the very best of business,</p>");
            sb.AppendLine("<p>Osell.com  Service Team</p>");
            sb.AppendLine("</div></div></div></div>");
            return sb.ToString();
        }

        public class OpportunityProductInModel
        {
            /// <summary>
            /// ID
            /// </summary>
            public int ID { get; set; }
            /// <summary>
            /// 产品类型
            /// </summary>
            public int ProductType { get; set; }

            /// <summary>
            /// 产品ID
            /// </summary>
            public long? ProductID { get; set; }

            /// <summary>
            /// 产品名称
            /// </summary>
            public string ProductName { get; set; }
            /// <summary>
            /// 产品参数
            /// </summary>
            public string ProductParam { get; set; }
            /// <summary>
            /// 产品图片
            /// </summary>
            public string ProductImg { get; set; }
            /// <summary>
            /// 产品数量
            /// </summary>
            public int ProductCount { get; set; }
            /// <summary>
            /// 产品价格范围
            /// </summary>
            public string PriceRange { get; set; }
            /// <summary>
            /// 货币类型
            /// </summary>
            public string Currency { get; set; }
            /// <summary>
            /// 备注
            /// </summary>
            public string Remark { get; set; }
            /// <summary>
            /// 是否删除
            /// </summary>
            public bool IsDel { get; set; }
        }
    }
}
