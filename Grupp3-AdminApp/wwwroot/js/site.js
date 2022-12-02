


function generateChart() {
    var data = JSON.parse(document.getElementById("elevatorslist").value);

    var elevatorList = [];

    for (var i = 0; i < data.length; i++) {
        elevatorList.push({
            elevator: data[i].Name,
            count: data[i].ErrandCount
        });
    }

    new Chart(
        document.getElementById("chart"),
        {
            type: "bar",
            data: {
                labels: elevatorList.map(row => row.elevator),
                datasets: [
                    {
                        label: "Errands by elevator",
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