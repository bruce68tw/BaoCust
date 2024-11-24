var _me = {

    init: function () {
        _ajax.getJson('GetData', null, function (rows) {
            _chart.line('chart', rows, '#3e95cd');
        });
    },

}; //class