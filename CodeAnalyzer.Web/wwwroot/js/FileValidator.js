$(function () {
    jQuery.validator.addMethod('notselected',
        function (value, element) {            
            return (value != "");
        });
    jQuery.validator.addMethod('fileformat',
        function (value, element) {
            return /^.*(.cs|.txt)$/.test(value);
        });
    jQuery.validator.addMethod('filesize',
        function (value, element) {
            return element.files[0].size < 512000;
        });

    jQuery.validator.unobtrusive.adapters.addBool('notselected');
    jQuery.validator.unobtrusive.adapters.addBool('fileformat');
    jQuery.validator.unobtrusive.adapters.addBool('filesize');

}(jQuery));