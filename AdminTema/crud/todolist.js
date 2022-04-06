///////////////////////////////////////KATEGORİLER//////////////////////////////////////////////
//KATEGORİ SİLME
$(document).on("click", ".TodoDelete", function () {
  var id = $(this).attr("data-id");
  $("#TodoID").val(id);
});

$("#TodoDelete").click(function () {
  var id = $("#TodoID").val();
  $.ajax({
    type: "POST",
    url: "/Dashboard/TodoDelete/",
    data: { "id": id },
    dataType: "json",
    success: function (data) {
      window.location.href = "/Dashboard/Index/";
    }
  });
});
//KATEGORİ GÜNCELLEME
$(document).on("click", ".TodoEdit", function () {
  var id = $(this).attr("data-id");
  $("#TodoID").val(id);
  $.ajax({
    type: "POST",
    url: "/Dashboard/TodoGetItem/" + id,
    dataType: "json",
    success: function (data) {
      $("#EditCategoryID").val(data.TodoID);
      $("#EditTodoMail").val(data.TodoMail);
      $("#EditTodoContent").val(data.TodoContent);
      $("#EditTodoDate").val(data.TodoDate);
    }
  });
});
$("#TodoEdit").click(function () {
  var updatedata = $("#TodoEditForm").serialize();
  $.ajax({
    type: "POST",
    url: "/Dashboard/TodoUpdate/",
    data: updatedata,
    dataType: "json",
    success: function () {
      window.location.href = "/Dashboard/Index/";
    }
  });
});
//KATEGORİ EKLEME
$("#TodoAdd").click(function () {
  var updatedata = $("#TodoAddForm").serialize();
  $.ajax({
    type: "POST",
    url: "/Dashboard/TodoAdd/",
    data: updatedata,
    dataType: "json",
    success: function () {
      window.location.href = "/Dashboard/Index/";
    }
  });
});
//KATEGORİ DETAY
$(document).on("click", ".TodoDetail", function () {
  var id = $(this).attr("data-id");
  $.ajax({
    type: "POST",
    url: "/Dashboard/TodoGetItem/" + id,
    dataType: "json",
    success: function (data) {
      $("#DetailCategoryID").val(data.TodoID);
      $("#DetailTodoMail").val(data.TodoMail);
      $("#DetailTodoContent").val(data.TodoContent);
      $("#DetailTodoDate").val(data.TodoDate);
    }
  });
});

$(document).on("click", ".TodoUpdateT", function () {
  var id = $(this).attr("id");
  $.ajax({
    type: "POST",
    url: "/Dashboard/TodoUpdateT/" + id,
    dataType: "json",
    success: function (data) {
      window.location.href = "/Dashboard/Index/";
    }
  });
});

$(document).on("click", ".TodoUpdateF", function () {
  var id = $(this).attr("id");
  $.ajax({
    type: "POST",
    url: "/Dashboard/TodoUpdateF/" + id,
    dataType: "json",
    success: function (data) {
      window.location.href = "/Dashboard/Index/";
    }
  });
});
