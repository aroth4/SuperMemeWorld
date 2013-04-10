using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpKit.JavaScript;
using SharpKit.Html;

namespace SuperMemeWorld
{
    [JsType(JsMode.Global, Filename = "Site.js")]
    class Site : HtmlContext
    {
        public static void btnHello_click(SharpKit.Html4.HtmlDomEventArgs e)
        {
            document.body.appendChild(document.createTextNode("Hello SharpKit!"));
        }
    }
}
