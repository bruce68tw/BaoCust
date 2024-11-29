//1.declare _me
var _me = {

    init: function () {        
        //datatable config
        var config = {
            //2.查詢結果欄位
            columns: [
                { data: 'Name' },
                { data: 'StartTime' },
                { data: 'EndTime' },
                { data: 'IsMove' },
                { data: 'ReplyTypeName' },
                { data: 'PrizeTypeName' },
                { data: 'PrizeNote' },
                { data: 'StageCount' },
                { data: '_Fun' },
            ],
            //3.查詢結果欄位格式設定
            columnDefs: [
				{ targets: [1], render: function (data, type, full, meta) {
                    return _date.mmToUiDt2(data);
                }},
				{ targets: [2], render: function (data, type, full, meta) {
                    return _date.mmToUiDt2(data);
                }},
                //#region ...
				{ targets: [3], render: function (data, type, full, meta) {
                    return _crudR.dtYesEmpty(data);
                }},
				{ targets: [8], render: function (data, type, full, meta) {
                    return _crudR.dtCrudFun(full.Id, full.Name, true, true, true);
                }},
                //#endregion
            ],
        };

        //4.CRUD initial
        _me.edit0 = new EditOne();
        _me.mStage = new EditMany('Id', 'eformStage', 'tplStage', 'tr', 'Sort');
        _crudR.init(config, [_me.edit0, _me.mStage]);

        //#region declare custom function
        _me.divStage = $('#tbodyStage');
        _me.edit0.fnAfterOpenEdit = _me.edit0_afterOpenEdit;
        _me.edit0.fnWhenSave = _me.edit0_fnWhenSave;
        //#endregion
    },

    //#region your custom function ...
    //set stage answer word
    edit0_afterOpenEdit: function (fun, json) {
        /*
        $('#tbodyStage tr').each(function () {
            _me.setAnswer2($(this));
        });
        */
    },

    //return error msg if any
    edit0_fnWhenSave: function () {
        //set StageCount
        var count = _table.getRowCount(_me.divStage, 'Id');
        _iread.set('StageCount', count, _me.edit0.eform);
        //return '';
    },

    /*
    //tr: object
    setAnswer2: function (tr) {
        //todo
        //_obj.getN('Answer2', tr).text(_itext.get('Answer', tr) == '' ? '' : '(已設定)');
    },
    */

    //TODO: add your code
    //onclick viewFile, called by XiFile component
    onViewFile: function (table, fid, elm) {
        _me.mStage.onViewFile(table, fid, elm);
    },

    //on set answer of stage
    onTestAns: function (me) {
        //debugger;
        var tr = $(me).closest('tr');
        _tool.showArea('測試答案', '', _crudE.isEditMode(), function (input) {
            //check empty
            if (_str.isEmpty(input)) {
                _tool.msg('不可空白。');
                return;
            }

            //check local
            var rightMsg = '答案正確。';
            var ans = _itext.get('Answer', tr);
            if (ans == input) {
                _tool.msg(rightMsg);
                return;
            }

            //case new row
            var errorMsg = '您的答案與輸入欄位不符合。';
            if (_me.mStage.isNewTr(tr)) {
                _tool.msg(errorMsg);
                return;
            }

            //check remote if need
            _ajax.getStrA('CheckAnswer', { id: _me.mStage.getKey(tr), input: input }, function (data) {
                _tool.msg((data == '1') ? rightMsg : errorMsg);
            });
        });
    },
    //#endregion
};