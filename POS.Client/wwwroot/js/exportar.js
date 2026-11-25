window.exportarPDF = function (elementoId, nombreArchivo) {
    var element = document.getElementById(elementoId);
    var opt = {
        margin: 1,
        filename: nombreArchivo + '.pdf',
        image: { type: 'jpeg', quality: 0.98 },
        html2canvas: { scale: 2 },
        jsPDF: { unit: 'in', format: 'letter', orientation: 'portrait' }
    };
    html2pdf().set(opt).from(element).save();
};

window.exportarExcel = function (tablaId, nombreArchivo) {
    var table = document.getElementById(tablaId);
    var wb = XLSX.utils.table_to_book(table, { sheet: "Comprobante" });
    XLSX.writeFile(wb, nombreArchivo + '.xlsx');
};