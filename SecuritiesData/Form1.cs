using Newtonsoft.Json;
using OfficeOpenXml;
using SecuritiesData.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SecuritiesData.Service;
using System.Text.RegularExpressions;
using OfficeOpenXml.Style;

namespace SecuritiesData
{
    public partial class Form1 : Form
    {
        string filepath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public Form1()
        {
            InitializeComponent();
            cbType.ValueMember = "value";
            cbType.DisplayMember = "text";
            cbType.DataSource = SelectType.SelectCategories();
            

        }

        private async void btnQuery_Click(object sender, EventArgs e)
        {
            string Type = (cbType.SelectedValue as SelectType).GetValue();//分類項目
            string date = dtpDate.Value.Date.AddDays(1).CompareTo(DateTime.Now)>0?
                $"{dtpDate.Value.Date.AddDays(-1):yyyyMMdd}":
                $"{dtpDate.Value.Date:yyyyMMdd}";//查詢日期
            string url = $"http://www.twse.com.tw/exchangeReport/BWIBBU_d?response=json&date={date}&selectType={Type}";

            try
            {
                await QuerySecurties(url);
                
            }
            catch (Exception ex)
            {
                ErrorService.WriteLog(ex.ToString());
                lblMsg.Text = "-99發生例外錯誤";
                return;
            }


            if (!string.IsNullOrEmpty(tbReceiver.Text) &&
                CheckMailFormat(@"^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z]+$", tbReceiver.Text))
            {


                SendMailService service = new SendMailService();
                service.SendGMail("", tbReceiver.Text, filepath + "/File/Security.xls");
                lblMsg.Text = "寄送成功";

            }
            else
            {
                lblMsg.Text = "不填收件人是要我送到哪個神奇的地方?";
            }





        }
        //去證交所把股票資料爬下來
        public async Task QuerySecurties(string url)
        {
            Security temp = new Security();
               HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("applicatioc/json"));
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                temp = JsonConvert.DeserializeObject<Security>(responseBody);
            }
            //直接去產生Excel

            ExcelService.GererateExcel(temp);
        }
       
        public bool CheckMailFormat(string Pattern, string Text)
        {
            Regex pattern = new Regex(Pattern, RegexOptions.IgnoreCase);
            Match match = pattern.Match(Text);
            if (match.Success)
                return true;
            else
                return false;
        }
    }
}
