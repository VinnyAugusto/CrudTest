var SPMaskBehavior = function (val) {
    return val.replace(/\D/g, '').length === 11 ? '(00) 00000-0000' : '(00) 0000-00009';
};

var spOptions = {
    onKeyPress: function (val, e, field, options) {
        field.mask(SPMaskBehavior.apply({}, arguments), options);
    }
};


function MaskTextField(fieldId) {
    var maxlength = $(fieldId).attr("maxlength");
    var strBase = "";

    for (var i = 0; i < maxlength; i++)
        strBase += 'z';

    $(fieldId).mask(strBase, { translation: { 'z': { pattern: /[a-zA-Z ]/, recursive: true } } });
}