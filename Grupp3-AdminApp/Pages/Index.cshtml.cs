using Grupp3_Elevator.Data;
using Grupp3_Elevator.Models;
using Grupp3_Elevator.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Grupp3_Elevator.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IElevatorService _elevatorService;
        public IndexModel(ApplicationDbContext context, ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
        }
        public string ErrandsAmount { get; set; }
        public string ElevatorsAmount { get; set; }
        public string TechnichansAmount { get; set; }
        public string CommentsAmount { get; set; }
        public List<int> ErrandsPerElevatorOne { get; set; }

        public List<ElevatorDeviceItem> Elevators { get; set; }
        public List<int> Errands { get; set; }

        public List<int> GetJson()
        {
            var data = _context.Errands.ToList();
            return data;
        }

        public async Task OnGet()
        {
            ErrandsAmount = _context.Errands.Select(a => a.Id).Count().ToString();
            ElevatorsAmount = _context.Elevators.Select(a => a.Id).Count().ToString();

            TechnichansAmount = _context.Technicians.Select(a => a.Id).Count().ToString();
            CommentsAmount = _context.ErrandComments.Select(a => a).Count().ToString();

            var chartData = @"{type: 'bar',responsive: true,data:{
            labels: ['Red', 'Blue', 'Yellow', 'Green', 'Purple', 'Orange'],
            datasets: [{
                label: '# of Votes',
                data: [],
                backgroundColor: [
                'rgba(255, 99, 132, 0.2)',
                'rgba(54, 162, 235, 0.2)',
                'rgba(255, 206, 86, 0.2)',
                'rgba(75, 192, 192, 0.2)',
                'rgba(153, 102, 255, 0.2)',
                'rgba(255, 159, 64, 0.2)'
                    ],
                borderColor: [
                'rgba(255, 99, 132, 1)',
                'rgba(54, 162, 235, 1)',
                'rgba(255, 206, 86, 1)',
                'rgba(75, 192, 192, 1)',
                'rgba(153, 102, 255, 1)',
                'rgba(255, 159, 64, 1)'
                    ],
                borderWidth: 1
            }]
        },
        options:
        {
            scales:
            {
                yAxes: [{
                    ticks:
                    {
                        beginAtZero: true
                    }
                }]
            }
        }
    }";

            Chart = JsonConvert.DeserializeObject<ChartJs>(chartData);

            var res = GetJson();  //get the data

            //must remember to initialize the array....
            Chart.data.datasets[0].data = new int[res.Count()];
            for (int i = 0; i < res.Count(); i++)
            {
                Chart.data.datasets[0].data[i] = res[i];
            }
            ChartJson = JsonConvert.SerializeObject(Chart, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            });
        }

    }

    ////datalist = new List<MyChartTestModel>();
    ////datalist.Add(new MyChartTestModel { MyLabel = "Red", MyData = "12" });
    ////datalist.Add(new MyChartTestModel { MyLabel = "Blue", MyData = "19" });
    ////datalist.Add(new MyChartTestModel { MyLabel = "Yellow", MyData = "3" });
    ////datalist.Add(new MyChartTestModel { MyLabel = "Green", MyData = "5" });
    ////datalist.Add(new MyChartTestModel { MyLabel = "Purple", MyData = "2" });
    ////datalist.Add(new MyChartTestModel { MyLabel = "Orange", MyData = "3" });

}

        public class ChartJs
        {
            public string type { get; set; }
            public bool responsive { get; set; }
            public Data data { get; set; }
            public Options options { get; set; }
        }

        public class Data
        {
            public string[] labels { get; set; }
            public Dataset[] datasets { get; set; }
        }

        public class Dataset
        {
            public string label { get; set; }
            public int[] data { get; set; }
            public string[] backgroundColor { get; set; }
            public string[] borderColor { get; set; }
            public int borderWidth { get; set; }
        }

        public class Options
        {
            public Scales scales { get; set; }
        }

        public class Scales
        {
            public Yax[] yAxes { get; set; }
        }

        public class Yax
        {
            public Ticks ticks { get; set; }
        }

        public class Ticks
        {
            public bool beginAtZero { get; set; }
        }

        //public class MyChartTestModel
        //{
        //    public string MyData { get; set; }
        //    public string MyLabel { get; set; }
        //}       

        //[BindProperty]
        //public List<MyChartTestModel> datalist { get; set; }
        //public void OnGet()
        //{

        //    datalist = new List<MyChartTestModel>();
        //    datalist.Add(new MyChartTestModel { MyLabel = "Red", MyData = "12" });
        //    datalist.Add(new MyChartTestModel { MyLabel = "Blue", MyData = "19" });
        //    datalist.Add(new MyChartTestModel { MyLabel = "Yellow", MyData = "3" });
        //    datalist.Add(new MyChartTestModel { MyLabel = "Green", MyData = "5" });
        //    datalist.Add(new MyChartTestModel { MyLabel = "Purple", MyData = "2" });
        //    datalist.Add(new MyChartTestModel { MyLabel = "Orange", MyData = "3" });
        //}



    }   
}