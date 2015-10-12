$(function () {

    // Home logo hover
    $("#logo").hover(
        function () {
            $(this).attr('src', '/Images/yackimoHover.png');
        },
        function () {
            $(this).attr('src', '/Images/yackimoHome.png');
        }
    );

    var ajaxFormSubmit = function () {
        var $form = $(this);

        var options = {
            url: $form.attr("action"),
            type: $form.attr("method"),
            data: $form.serialize()
        };

        // When request is complete and successful
        $.ajax(options).done(function (data) { // Response from server is in data object
            // Find dom (document object model) element on the page to be updated
            var $target = $($form.attr("data-yackimo-target"));
            var $newHtml = $(data);

            // Replace target with html we got back from server
            $target.replaceWith($newHtml);
            $newHtml.effect("highlight");
        });

        // Prevent browser from perfoming its default action
        // which is to navigating away and going to the server by itself and redrawing the page
        return false;
    };

    var submitAutocompleteForm = function (event, ui) {
        var $input = $(this);
        $input.val(ui.item.label);

        var $form = $input.parents("form:first");
        $form.submit();
    };

    var createAutocomplete = function () {
        // When jquery invokes this function it will pass along the input as the this parameter
        var $input = $(this);
        var options = {
            // take the url embeded in the data dash attr 
            // pull it out using the attr method of jquery and put it on the source property on the options object
            source: $input.attr("data-yackimo-autocomplete"),
            select: submitAutocompleteForm
        };

        // wire up autocomplete by walk up to the input, invoke the autocomplete method and
        // pass in the options object
        $input.autocomplete(options);
    };

    // Use jquery selector to find all the forms with data attr and set to value true
    // wire up submit event to send respose to js instead of server
    // in this case, send it to function 'ajaxFormSubmit'
    $("form[data-yackimo-ajax='true']").submit(ajaxFormSubmit);

    // Find all inputs that have the data dash attr
    // and for each call 'createAutocomplete' function
    $("input[data-yackimo-autocomplete]").each(createAutocomplete);
});