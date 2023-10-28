// Lấy tất cả các phần tử về số lượng, giá tiền, subtotal và total
const quantities = document.querySelectorAll('#cart input[type="number"]');
const prices = document.querySelectorAll('#cart tbody td:nth-child(4)');
const subtotals = document.querySelectorAll('#cart tbody td:nth-child(6)');
const cartSubtotal = document.querySelector('#subtotal table tr:first-child td:last-child');
const shipping = document.querySelector('#subtotal table tr:nth-child(2) td:last-child');
const total = document.querySelector('#subtotal table tr:last-child td:last-child');

// Tạo một hàm để tính toán lại giá trị cho các phần tử khác
function recalculate() {
  let subtotalVal = 0;
  let quantityTotal = 0;

  // Tính toán lại subtotal cho từng sản phẩm và tổng giá trị của số lượng
  for (let i = 0; i < quantities.length; i++) {
    const quantity = parseInt(quantities[i].value);
    const price = parseFloat(prices[i].textContent.replace('$', ''));
    const subtotal = quantity * price;
    subtotals[i].textContent = `$${subtotal.toFixed(2)}`;
    subtotalVal += subtotal;
    quantityTotal += quantity;
  }

  // Tính toán tổng giá trị của đơn hàng (subtotal, shipping và total)
  cartSubtotal.textContent = `$${subtotalVal.toFixed(2)}`;
  if (subtotalVal === 0) {
    shipping.textContent = '$0';
    total.textContent = '$0';
  } else {
    shipping.textContent = 'Free';
    total.textContent = `$${subtotalVal.toFixed(2)}`;
  }
}

// Lắng nghe sự kiện khi số lượng thay đổi và gọi hàm tính toán lại giá trị cho các phần tử khác
for (let i = 0; i < quantities.length; i++) {
  quantities[i].addEventListener('change', recalculate);
}

// Tính toán lại giá trị cho các phần tử khác khi trang được tải
recalculate();

