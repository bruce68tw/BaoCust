//1.declare _me
var _me = {

    init: function () {        
        //datatable config
        var config = {
            //2.查詢結果欄位
            columns: [
                { data: 'Name' },
                { data: 'StartTime' },
                //#region ...
                { data: 'EndTime' },
                { data: 'IsBatch' },
                { data: 'IsMove' },
                { data: 'IsMoney' },
                { data: 'GiftName' },
                { data: 'StageCount' },
                { data: '_Fun' },
                //#endregion
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
                    return _crud.dtYesEmpty(data);
                }},
				{ targets: [4], render: function (data, type, full, meta) {
                    return _crud.dtYesEmpty(data);
                }},
				{ targets: [5], render: function (data, type, full, meta) {
                    return _crud.dtYesEmpty(data);
                }},
				{ targets: [8], render: function (data, type, full, meta) {
                    return _crud.dtCrudFun(full.Id, full.Name, true, true, true);
                }},
                //#endregion
            ],
        };

        //4.CRUD initial
        _me.edit0 = new EditOne();
        _me.mStage = new EditMany('Id', 'eformStage', 'tplStage', 'tr', 'Sort');
        _crud.init(config, [_me.edit0, _me.mStage]);

        //#region declare custom function
        _me.divStage = $('#tbodyStage');
        _me.edit0.fnAfterOpenEdit = _me.edit0_afterOpenEdit;
        _me.edit0.fnWhenSave = _me.edit0_fnWhenSave;
        //#endregion
    },

    //#region your custom function ...
    //set stage answer word
    edit0_afterOpenEdit: function (fun, json) {
        $('#tbodyStage tr').each(function () {
            _me.setAnswer2($(this));
        });
    },

    //return error msg if any
    edit0_fnWhenSave: function () {
        //set StageCount
        var count = _table.getRowCount(_me.divStage, 'Id');
        _iread.set('StageCount', count, _me.edit0.eform);
        //return '';
    },

    //tr: object
    setAnswer2: function (tr) {
        _obj.getN('Answer2', tr).text(_itext.get('Answer', tr) == '' ? '' : '(已設定)');
    },

    //TODO: add your code
    //onclick viewFile, called by XiFile component
    onViewFile: function (table, fid, elm) {
        _me.mStage.onViewFile(table, fid, elm);
    },

    //on set answer of stage
    onAnswer: function (me) {
        var tr = $(me).closest('tr');
        _tool.showArea(title, _itext.get(fid, tr), _crud.isEditMode(), function (result) {
            _itext.set(fid, result, tr);
        });
    },
    //#endregion
};