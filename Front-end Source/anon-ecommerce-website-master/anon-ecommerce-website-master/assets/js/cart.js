// Lấy tất cả các checkbox
var checkboxes = document.querySelectorAll('input[type="checkbox"]');

// Lắng nghe sự kiện thay đổi trên mỗi checkbox
for (var i = 0; i < checkboxes.length; i++) {
  checkboxes[i].addEventListener("change", function () {
    // Lấy giá trị subtotal của hàng
    var subtotal = Number(
      this.parentElement.parentElement
        .querySelector("td:last-child")
        .innerText.replace("đ", "")
    );

    if (this.checked) {
      // Nếu checkbox được chọn, thêm giá trị của hàng vào tổng cộng
      addToTotal(subtotal);
    } else {
      // Nếu checkbox bị bỏ chọn, trừ giá trị của hàng khỏi tổng cộng
      subtractFromTotal(subtotal);
    }
  });
}

// Hàm thêm vào tổng cộng
function addToTotal(amount) {
  var totalElement = document.querySelector(".subtotal td:last-child");
  var total = Number(totalElement.innerText.replace(/[^0-9]/g, ""));
  total += amount;
  totalElement.innerText = total + "đ";
}

// Hàm trừ khỏi tổng cộng
function subtractFromTotal(amount) {
  var totalElement = document.querySelector(".subtotal td:last-child");
  var total = Number(totalElement.innerText.replace(/[^0-9]/g, ""));
  total -= amount;
  totalElement.innerText = total + "đ";
}
