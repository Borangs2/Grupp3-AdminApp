var db = 

function generateChart() {
    const data = [
        { elevator: "Hiss 1", count: 10 },
        { elevator: "Hiss 1", count: 20 },
        { elevator: "Hiss 1", count: 15 },
        { elevator: "Hiss 1", count: 25 },
        { elevator: "Hiss 1", count: 22 },
        
    ];

    new Chart(
        document.getElementById('chart'),
        {
            type: 'bar',
            data: {
                labels: data.map(row => row.elevator),
                datasets: [
                    {
                        label: 'Errands by elevator',
                        data: data.map(row => row.count)
                    }
                ]
            }
        }
    );
};