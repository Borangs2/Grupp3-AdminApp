


function generateChart() {
    var data = JSON.parse(document.getElementById('elevatorslist').value);

    var elevatorList = [];

    for (var i = 0; i < data.length; i++)
    {
        elevatorList.push({
            elevator: data[i].Name, count: data[i].ErrandCount
        });
    }

    //const data = [
    //    { elevator: getElementById('elevatorslist'), count: 15 },
    //    { elevator: "Hiss 1", count: 10 },
    //    { elevator: "Hiss 1", count: 20 },
    //    { elevator: "Hiss 1", count: 25 },
    //    { elevator: "Hiss 1", count: 22 },
        
    //];

    new Chart(
        document.getElementById('chart'),
        {
            type: 'bar',
            data: {
                labels: elevatorList.map(row => row.elevator),
                datasets: [
                    {
                        label: 'Errands by elevator',
                        data: data.map(row => row.ErrandCount)
                    }
                ]
            },
            options: {
                scales: {
                    y: {
                        ticks: {
                            precision: 0
                        },
                        suggestedMax: 5
                    }
                
                }
            },
            
        }
    );
};