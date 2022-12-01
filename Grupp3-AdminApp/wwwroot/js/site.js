function generateChart() {
    const data = [
        { hiss: "Hiss 1", count: 10 },
        { hiss: "Hiss 1", count: 20 },
        { hiss: "Hiss 1", count: 15 },
        { hiss: "Hiss 1", count: 25 },
        { hiss: "Hiss 1", count: 22 },
        
    ];

    new Chart(
        document.getElementById('chart'),
        {
            type: 'bar',
            data: {
                labels: data.map(row => row.hiss),
                datasets: [
                    {
                        label: 'Acquisitions by year',
                        data: data.map(row => row.count)
                    }
                ]
            }
        }
    );
};