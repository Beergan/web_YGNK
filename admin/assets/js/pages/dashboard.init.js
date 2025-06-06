! function(i) {
    "use strict";
    var e=function() { };
    e.prototype.respChart=function(e,r,a,t) {
        var o=e.get(0).getContext("2d"),
            s=i(e).parent();

        function n() {
            e.attr("width",i(s).width());
            switch(r) {
                case "Line":
                    new Chart(o,{
                        type: "line",
                        data: a,
                        options: t
                    });
                    break;
                case "Doughnut":
                    new Chart(o,{
                        type: "doughnut",
                        data: a,
                        options: t
                    });
                    break;
                case "Pie":
                    new Chart(o,{
                        type: "pie",
                        data: a,
                        options: t
                    });
                    break;
                case "Bar":
                    new Chart(o,{
                        type: "bar",
                        data: a,
                        options: t
                    });
                    break;
                case "Radar":
                    new Chart(o,{
                        type: "radar",
                        data: a,
                        options: t
                    });
                    break;
                case "PolarArea":
                    new Chart(o,{
                        data: a,
                        type: "polarArea",
                        options: t
                    })
            }
        }
        i(window).resize(n),n()
    },e.prototype.init=function() {
        this.respChart(i("#transactions-chart"),"Line",{
            labels: ["January","February","March","April","May","June","July","August","September","October"],
            datasets: [{
                label: "Conversion Rate",
                fill: !1,
                backgroundColor: "#5d6dc3",
                borderColor: "#5d6dc3",
                data: [44,60,-33,58,-4,57,-89,60,-33,58]
            },{
                label: "Average Sale Value",
                fill: !1,
                backgroundColor: "#3ec396",
                borderColor: "#3ec396",
                borderDash: [5,5],
                data: [-68,41,86,-49,2,65,-64,86,-49,2]
            }]
        },{
            responsive: !0,
            tooltips: {
                mode: "index",
                intersect: !1
            },
            hover: {
                mode: "nearest",
                intersect: !0
            },
            scales: {
                xAxes: [{
                    display: !0,
                    gridLines: {
                        color: "rgba(0,0,0,0.1)"
                    }
                }],
                yAxes: [{
                    gridLines: {
                        color: "rgba(255,255,255,0.05)",
                        fontColor: "#fff"
                    },
                    ticks: {
                        max: 100,
                        min: -100,
                        stepSize: 20
                    }
                }]
            }
        });
        this.respChart(i("#sales-history"),"Bar",{
            labels: ["January","February","March","April","May","June","July"],
            datasets: [{
                label: "Sales Analytics",
                backgroundColor: "#3ec396",
                borderColor: "#3ec396",
                borderWidth: 1,
                hoverBackgroundColor: "#3ec396",
                hoverBorderColor: "#3ec396",
                data: [65,59,80,81,56,89,40,20]
            }]
        },{
            scales: {
                yAxes: [{
                    gridLines: {
                        color: "rgba(255,255,255,0.05)",
                        fontColor: "#fff"
                    },
                    ticks: {
                        max: 100,
                        min: 20,
                        stepSize: 20
                    }
                }],
                xAxes: [{
                    gridLines: {
                        color: "rgba(0,0,0,0.1)"
                    }
                }]
            }
        })
    },i.ChartJs=new e,i.ChartJs.Constructor=e
}(window.jQuery),
function(e) {
    "use strict";
    window.jQuery.ChartJs.init()
}();