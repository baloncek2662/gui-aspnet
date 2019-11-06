// Write your JavaScript code.
$(function() {
    $('#from-dropdown').datetimepicker();
});

$(function() {
    $('#to-dropdown').datetimepicker();
});

function selectCategory(value) {
    $('#category-value').text(value);
}