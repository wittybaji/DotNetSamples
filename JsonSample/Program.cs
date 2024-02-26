using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;


namespace JsonSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var test = new Test() { Id = 200, Time = TimeSpan.FromMilliseconds(600001) };
            string result = JsonConvert.SerializeObject(test);
            Console.WriteLine(result);

            string jsonPath = @"D:\Desktop\tcs.backup.20240131.json";
            string content = File.ReadAllText(jsonPath);
            TcsResponse response = JsonConvert.DeserializeObject<TcsResponse>(content);
            var orderedResponse = new TcsResponse()
            {
                Code = response.Code,
                Msg = response.Msg,
                Data = response.Data
            };
            //orderedResponse.Data.List = response.Data.List.OrderBy(x => x.job_code).ToArray();
            //string orderedResponseContent = JsonConvert.SerializeObject(orderedResponse);
            //File.WriteAllText(@"D:\Desktop\tcs.backup.20240131.orderbyjobcode.json", orderedResponseContent);
            //int maxJobCode = response.Data.List.Select(x => x.job_code != null && int.TryParse(x.job_code, out int jobCode) ? jobCode : 0).Max();
            //Console.WriteLine(maxJobCode);

            var female = response.Data.List.Where(x => x.item_text == "女" && x.join_work_date != null && x.join_work_date.Value.Year >= 2020).OrderBy(x => x.job_code).ToArray();
            string orderedResponseContent = JsonConvert.SerializeObject(female);
            File.WriteAllText(@"D:\Desktop\tcs.female.20240131.Sex2join_work_date2020.json", orderedResponseContent);
        }
    }

    internal class Test
    {
        public int Id { get; set; }
        public TimeSpan Time { get; set; }
    }

    #region TCS
    public class TcsResponse
    {
        public long Code { get; set; }
        public string Msg { get; set; }
        public Data Data { get; set; }
    }

    public class Data
    {
        public long Total { get; set; }
        public EmployeeInfo[] List { get; set; }
        public long PageNum { get; set; }
        public long PageSize { get; set; }
        public long Size { get; set; }
        public long StartRow { get; set; }
        public long EndRow { get; set; }
        public long Pages { get; set; }
        public long PrePage { get; set; }
        public long NextPage { get; set; }
        public bool IsFirstPage { get; set; }
        public bool IsLastPage { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
        public long NavigatePages { get; set; }
        public long[] NavigatepageNums { get; set; }
        public long NavigateFirstPage { get; set; }
        public long NavigateLastPage { get; set; }
    }

    public class EmployeeInfo
    {
        //public object Id { get; set; }
        //public object pk_psndoc { get; set; }
        public string job_code { get; set; }
        //public object UserCode { get; set; }
        public string Name { get; set; }
        public string pk_dept { get; set; }
        //public long? Sex { get; set; }
        //public string census_register { get; set; }
        public string pk_org { get; set; }
        public DateTime? join_work_date { get; set; }
        public DateTime join_sys_date { get; set; }
        public string pk_post { get; set; }
        //public object job_grade { get; set; }
        //public long Status { get; set; }
        public DateTime? hcm_update_time { get; set; }
        //public long base_doc_type { get; set; }
        //public long Enablestate { get; set; }
        //public long user_type { get; set; }
        public DateTime create_time { get; set; }
        public DateTime update_time { get; set; }
        //public string head_portrait { get; set; }
        //public object Photo { get; set; }
        public string pk_orgs { get; set; }
        public string pk_depts { get; set; }
        public string pk_posts { get; set; }
        public string item_text { get; set; }
        //public object job_grades { get; set; }
        //public long? sex_id { get; set; }
        //public object job_grade_id { get; set; }
        //public object Mobile { get; set; }
        public object Avatar { get; set; }
        //public object ThumbAvatar { get; set; }
        //public string Jobtitle { get; set; }
        //public string Jobname { get; set; }
    }
    #endregion
}
