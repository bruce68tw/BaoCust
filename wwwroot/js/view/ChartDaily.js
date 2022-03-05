var _me = {

    init: function () {
        _me.form = $('#formRead');
    },

    //on select bao item
    onItem: function () {
        var baoId = _iselect.get('BaoId', _me.form);
        if (_str.isEmpty(baoId))
            return;

        _ajax.getJson('GetData', { id: baoId }, function (rows) {
            _chart.line('chart', rows, '#3e95cd');
        });
    },

}; //class