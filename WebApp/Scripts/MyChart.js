var ctx = document.getElementById('myChart').getContext('2d');

var chart = new Chart(ctx, {
    // The type of chart we want to create
    type: 'line',

    // The data for my dataset
    data: {
        labels: [],
        datasets: [{
            label: 'Laboratory Values',
            //backgroundColor: 'rgb(255, 99, 132)',
            borderColor: 'rgb(255,47,138)',
            data: []
        }, {
          label: 'Sensors Values',
          //backgroundColor: 'rgb(255, 99, 132)',
          // borderColor: 'rgb(255,47,138)',
          fill: false,
          data: []
      }]
    },

    // Configuration options go here
    options: {
        scales: {
            yAxes: [{
                ticks: {
                    // beginAtZero:true Чтобы графики не сливались
                },
                scaleLabel: {
                    display: true,
                    labelString: 'Time',
                    fontSize: 20
                }
            }],
        }
    }
});

class DataProvider {
    constructor(){
        this.baseUrl = `http://localhost:5000`;
        // this.baseUrl = `http://${window.location.hostname}:5000`;
    };

    fetchData = async (start, end) => {
        const data = await fetch(`${this.baseUrl}/home/set?start=${start}&end=${end}`);
        return data.json();
    };
}

const setChartData = (chart, data) => {
    const getTimeLabSensArrays = (data) => {
        const timeArr = [];
        const labValArr = [];
        const sensValArr = [];
        for (let i = 0; i < data.length; i++){
            timeArr.push(data[i].Time);
            labValArr.push(data[i].LabVal);
            sensValArr.push(data[i].SensVal);
        };
        return {timeArr, labValArr, sensValArr};
    };

    const {timeArr, labValArr, sensValArr} = getTimeLabSensArrays(data); 
    chart.data.labels = [];
    chart.data.datasets[0].data = [];
    chart.data.labels = [].concat(timeArr); //x
    chart.data.datasets[0].data = [].concat(labValArr); //y1
    chart.data.datasets[1].data = [].concat(sensValArr); //y2
    chart.update();
};

const dataProvider = new DataProvider();

const getData = () => {
  const getDate = (input) => {
    let date = input.value.split(' ')[0].split('.');
    let time = input.value.split(' ')[1] + ':00';
    let newDate = '';
    for (let i = date.length-1; i >= 1; i--){
      newDate += date[i] + '-';
    };
    newDate += date[0];
    return newDate+'T'+time;
  };

  const start = getDate(document.getElementById('startDateInput'));
  const end = getDate(document.getElementById('endDateInput'));
  // console.log(start, end);
  dataProvider.fetchData(start, end)
    .then((data) => {
        setChartData(chart, data)
    });
};