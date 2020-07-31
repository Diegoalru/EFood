/*
 * NO HACER CAMBIOS
 */

$("body").on("change", "#ddl", function () {
    $("#hf").val($(this).find("option:selected").text());
});

function googleTranslateElementInit() {
    new google.translate.TranslateElement({pageLanguage: 'es'}, 'google_translate_element');
}

/*
 * NO HACER CAMBIOS
 */