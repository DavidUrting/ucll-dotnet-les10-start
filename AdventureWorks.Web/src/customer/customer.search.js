import $ from "jquery";

if (document.getElementById("searchButton") !== null) {
    document.getElementById("searchButton").addEventListener("click", function (e) {
        // vorige resultaten leegmaken.
        $("#customers tbody tr").remove();

        // keyword achterhalen. Indien leeg: geen query doen.
        let keyword = document.getElementById("keywordInput").value;
        if (keyword) {
            $.post("/customer/search?keyword=" + keyword, function (result) {
                for (let i = 0; i < result.length; i++) {
                    $("#customers").find('tbody').append(
                        '<tr><td>' + result[i].id +
                        '</td><td>' + result[i].firstName +
                        '</td><td>' + result[i].lastName +
                        '</td><td>' + result[i].email +
                        '</td><td><a href="/customer/details/' + result[i].id + '">Details</a> | <a href="/customer/edit/' + result[i].id + '">Edit</a> | <a href="/customer/delete/' + result[i].id + '">Delete</a></td></tr>');
                }
            });
        }
        e.preventDefault();
    });
}

