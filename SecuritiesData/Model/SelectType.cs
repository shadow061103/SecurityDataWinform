using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecuritiesData.Model
{
    public class SelectType
    {
        private string text { get; set; }
        private string value { get; set; }

        public SelectType(string Text, string Value)
        {
            this.text = Text;
            this.value = Value;

        }
        public override string ToString()
        {
            return text;
        }
        public string GetValue()
        {
            return value;
        }
        public static List<SelectType> SelectCategories()
        {
            return new List<SelectType>() {
         new SelectType("全部","ALL"),
         new SelectType( "水泥工業","01"),
         new SelectType("食品工業", "02"),
          new SelectType("塑膠工業","03"),
          new SelectType("紡織纖維","04"),
          new SelectType("電機機械","05"),
          new SelectType("電器電纜","06"),
          new SelectType("化學生技醫療","07"),
          new SelectType("化學工業","21"),
          new SelectType("生技醫療業","22"),
          new SelectType("玻璃陶瓷","08"),
          new SelectType("造紙工業","09"),
          new SelectType("鋼鐵工業","10"),
          new SelectType("橡膠工業","11"),
         new SelectType("汽車工業", "12"),
          new SelectType("電子工業","13"),
          new SelectType("半導體業","24"),
          new SelectType("電腦及週邊設備業","25"),
          new SelectType("光電業","26"),
          new SelectType("通信網路業","27"),
          new SelectType("電子零組件業","28"),
          new SelectType("電子通路業","29"),
          new SelectType("資訊服務業","30"),
          new SelectType("其他電子業","31"),
          new SelectType("建材營造","14"),
          new SelectType("航運業","15"),
          new SelectType("觀光事業","16"),
          new SelectType("金融保險","17"),
          new SelectType("貿易百貨","18"),
          new SelectType("油電燃氣業","23"),
          new SelectType("綜合","19"),
          new SelectType("其他","20")

        };
        }



    }
}
