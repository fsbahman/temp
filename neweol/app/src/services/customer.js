import $ from 'jquery';

class Customer{
    search(keyword, next, err){
        $.ajax({
            type: 'POST',
            url: 'https://search-myelastic-sn3n2ouqbckog557vgogxacpd4.us-east-1.es.amazonaws.com/_search?pretty',
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Content-type", "application/json");
            },
            data: '{ "query": { "prefix" : { "_all" : "' + keyword + '" }}}'
        }).done(function (d) {
            if (next) next(d);
        }).fail(function (jqXhr) {
            console.log('failed to search');
            if (err) err();
        });
    }
}
export default Customer;