$(function () {
  $("#example1").DataTable({
    "responsive": true,
    "autoWidth": false,
    "language": {
      "emptyTable": "Gösterilecek ver yok.",
      "processing": "Veriler yükleniyor",
      "sDecimal": ".",
      "sInfo": "_TOTAL_ kayıttan _START_ - _END_ arasındaki kayıtlar gösteriliyor",
      "sInfoFiltered": "(_MAX_ kayıt içerisinden bulunan)",
      "sInfoPostFix": "",
      "sInfoThousands": ".",
      "sLengthMenu": "Sayfada _MENU_ kayıt göster",
      "sLoadingRecords": "Yükleniyor...",
      "sSearch": "Ara:",
      "sZeroRecords": "Eşleşen kayıt bulunamadı",
      "oPaginate": {
        "sFirst": "İlk",
        "sLast": "Son",
        "sNext": "Sonraki",
        "sPrevious": "Önceki"
      },
      "oAria": {
        "sSortAscending": ": artan sütun sıralamasını aktifleştir",
        "sSortDescending": ": azalan sütun sıralamasını aktifleştir"
      },
      "select": {
        "rows": {
          "_": "%d kayıt seçildi",
          "0": "",
          "1": "1 kayıt seçildi"
        }
      }
    }
  });
});
