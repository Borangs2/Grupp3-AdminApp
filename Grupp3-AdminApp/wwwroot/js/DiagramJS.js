<script type="text/javascript">
    jQuery(function ($) {
    var data1 = [12, 3, 4, 2, 12, 3, 4, 17, 22, 34, 54, 67];
    var data2 = [3, 9, 12, 14, 22, 32, 45, 12, 67, 45, 55, 7];
    var data3 = [23, 19, 11, 134, 242, 352, 435, 22, 637, 445, 555, 57];

    $(function () {
        $("#chart1").shieldChart({
            exportOptions: {
                image: false,
                print: false
            },
            axisY: {
                title: {
                    text: "Break-Down for selected quarter"
                }
            },
            dataSeries: [{
                seriesType: "line",
                data: data1
            }]
        });

    $("#chart2").shieldChart({
        exportOptions: {
        image: false,
    print: false
                },
    axisY: {
        title: {
        text: "Break-Down for selected quarter"
                    }
                },
    dataSeries: [{
        seriesType: "line",
    data: data2
                }, {
        seriesType: "line",
    data: data3
                }]
            });


        });

    });
</script>