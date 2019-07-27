/* For Tooltip to work */
$(function () {
    $('[data-toggle="tooltip"]').tooltip()
})
/* END */

/* Making the panel sizable */
$(".panel-left").resizable({
    handleSelector: ".splitter",
    resizeHeight: false
});

$(".panel-top").resizable({
    handleSelector: ".splitter-horizontal",
    resizeWidth: false
});
/* END */


/* Setting Active on left nav items */
$('.nav-list').on('click', 'li', function () {
    $('.nav-list li.active').removeClass('active');
    $(this).addClass('active');

    $("html, body").animate({ scrollTop: 0 }, "slow");    
});
/* END */




/* Load the page with the left nav and select the first one */
function loadData() {
    /* Load the navigation menu on the left */
    $("#nav-left").empty();
    navTitles = [];

    let liItems = [];
    for (let title in dataTable) {
        let fieldsData = dataTable[title];

        if(fieldsData && fieldsData.length > 0 && fieldsData[0] && fieldsData[0].length > 0 ){
            navTitles.push(title);
            let count = fieldsData[0].length;
        
            if (data.hasOwnProperty(title)) {
                liItems.push(`
                <li class="bg-dark py-0 list-group-item" id="${title}" style="cursor:pointer;"  data-toggle="tooltip" title="${title}">
                    <a class="nav-link" onClick="ShowFields('${title}')">${title}
                        <span class="badge badge-success badge-pill">${count}</span>
                    </a>
                </li>`);
            }
        }

       
    }
    $("#nav-left").append(liItems.join(""));

    /* Intialize the right panel with Alternative */
    if(navTitles && navTitles.length>0){
        let navItem = navTitles[0];
        ShowFields(navItem);
        $(`#${navItem}`).addClass("active");
    }
    
}


/* Load the Fields ont he right panel */
function ShowFields(title) {
    $("#panel-right-header").empty();
    $("#tables").empty();

    let summarized = {};
    let fieldsData = dataTable[title];
    fieldsData = fieldsData[0];
    for (let i in fieldsData) {
        let field = fieldsData[i];
        let category = field["Category"];
        if(category){
            category = category.trim()
            .replace("<", "").replace(">", "")
            .replace("(", "").replace(")", "")
            .replace("#", "")
            .replace(" ", "-");
        if (!(category in summarized)) {
            summarized[category] = [];
        }
        summarized[category].push(field);
        }
            
    }

    let columns = [];
    for (let title in fieldsData[0]) {
        columns.push({ "field": title, "title": title });
    }

    // set the title of the right panel
    $("#paner-right-header").text(`${title}`);

    for (let category in summarized) {
        $('#tables').append(`<h4 class="mt-3">Category: ${category} </h4>`);
        $('#tables').append(`<div class="" id=table-${category}></div>`)
        $(`#table-${category}`).bootstrapTable({ columns: columns, data: summarized[category] });
        $(`#table-${category}`).bootstrapTable('hideColumn', 'Category');
    }

    // Give a Card sytle appearance
    $('.fixed-table-body').addClass('card');
    $('thead').addClass('card-header');
}





/* Search for the given keyword */
function findData(dataObject, criteria) {
    let value = new RegExp(criteria, 'ig');
    let results = {};

    for (let title in dataObject) {
        if (!results.hasOwnProperty(title))
            results[title] = [];

        if (data.hasOwnProperty(title)) {
            results[title].push(search(title));
        }
    }

    function search(navTitle) {
        return data[navTitle].filter(o => {
            return value.test(JSON.stringify(o));
        })
    }

    return results;
}



/* Prepare the data */
let data = JSON.parse(dataString);
let navTitles = [];
let dataTable = findData(data, "");
loadData();


/* Search from the UI */
$("#searchform").submit(() =>{
    let searchQuery = $("#search_query").val();
    dataTable = findData(data, searchQuery);
    loadData();
})

$("#search_query").on('input propertychange paste', ()=>{
    let searchQuery = $("#search_query").val();
    dataTable = findData(data, searchQuery);
    loadData();
})