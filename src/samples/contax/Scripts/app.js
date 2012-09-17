var root_url = "/api"
    , collection_templates = {}
    , link_template
    , resource_template
    , form_field_template;

var get = function (url, callback) {

    var complete_url = root_url + url;
    $.getJSON(complete_url, callback);
//    $.ajax({ async: true, type: "GET", dataType: 'json', url: complete_url, success: callback });
};
var compile_templates = function() {
    link_template = Handlebars.compile($('#link-template').html());
    form_field_template = Handlebars.compile($('#form-field-template').html());
    resource_template = Handlebars.compile($('#resource-template').html());
};
var load_root = function (data) {
    var ul = $('<ul id="root" class="link-list">');
    data.collection.links.forEach(function (link) {
        var li = $("<li>");
        li.append(link_template(link));
        ul.append(li);
    })
    $('#content').append(ul);

};
var render_content = function(data) {
    if (data.collection) {
        render_collection(data.collection);

    }
    if (data.resource) {
        render_resource(data.resource);
    }
};
var render_resource = function (resource) {
    $('#collection-table').hide();
    var resource_view = resource_template(resource);
    $('#content').append(resource_view);
}
var render_collection = function(collection) {
    $('#content').empty();
    var container = $('#content');
    render_collection_links(collection,container);
    render_collection_items(collection,container);
    

};
var render_collection_links = function (collection, container) {
    var ul = $('<ul>');

    var create_li = $('<li>');
    var self_link = collection.self;
    self_link.prompt = "Create";
    var create_button = $("<button class='create'>Create</button>");

    create_button.on('click', function (e) {
        e.preventDefault();
        setup_collection_template(collection, container);
    });
    create_li.append(create_button);
    ul.append(create_li);
    collection.links.forEach(function (l) {
        var li = $('<li>');
        li.append(link_template(l));
        ul.append(li);
    });
    container.append(ul);



};
var render_collection_items = function (collection, container) {
    var data = collection.items;
    var self_link = collection.self;
    if (data.length == 0) {
        html = "<tr><td>There's no data for " + collection.self.prompt + "</td></tr>";
    }
    else {
        var headers = "";

        _.pluck(data[0].Data, 'prompt').forEach(function (h) { headers += "<th>" + h + "</th>"; });

        var rows = "";
        var len = data.length;

        for (var i = 0; i < len; i++) {
            var fields = data[i].Data;
            var item_links = data[i].Links;
            rows += "<tr>";
            var details_cell = "<td><a href=" + data[i].Href + " rel=" + self_link.rel + ">View</a></td>";
            rows += details_cell;
            var values = _.pluck(data[i].Data, "value");
            for (var j = 0; j < values.length; j++) {
                rows += "<td>" + values[j] + "</td>";
            }
            var link_cell = "<td>";
            for (var l = 0; l < item_links.length; l++) {
                link_cell += link_template(item_links[l]);
            }
            link_cell += "</td>";
            rows += link_cell


            rows += "</tr>";
        }
        var table = $('<table>', { id: 'collection-table' });
        table.addClass('table');
        table.html("<thead>" + headers + "</thead><tbody>" + rows + "</tbody>");
        container.append(table);


    }


};
var setup_collection_template = function (collection, container) {
    var cached_template = collection_templates[collection.self.rel];
    var back_button = $('<button>', { text: 'Back' }).addClass('btn back-btn').click(function () {
        $('#collection-table').show();
        $('.collection-form').hide();
    })
    $('#collection-table').hide();
    container.find('ul')
        .append('<li>').append(back_button);
    if (cached_template) {
        container.append(cached_template);
    }
    else {
        var form = render_template(collection);
        collection_templates[collection.self.rel] = form;
        container.append(form);
    }

};

var render_template = function (collection) {
    var form = []
    form.push("<form class='collection-form' id='" + collection.self.name + "' action='" + collection.self.href + "' >");
    var template_fields = collection.template.data;
    template_fields.forEach(function (f) {
        var form_field = form_field_template(f);
        form.push(form_field);
    });
    form.push("<button type='submit'>Submit</>");
    form.push("</form>");
    
    return form.join('');

}
$(function () {
    console.log("app start");
    compile_templates();
    get('', load_root);
    $(document).on('click','a',function (e) {
        e.preventDefault();
        href = this.pathname;
        get(href, render_content);
    });

});

