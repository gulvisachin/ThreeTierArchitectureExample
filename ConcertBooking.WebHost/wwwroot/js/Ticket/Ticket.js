function printTicket(bookingId) {
    var element = document.getElementById('booking-' + bookingId);
    element.classList.add('print-section');
    window.print();
    element.classList.remove('print-section')

}