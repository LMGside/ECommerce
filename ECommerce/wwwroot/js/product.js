async function add(stockId) {
    try {
        var username = document.getElementById("username");
        if (username == null) {
            window.location.href = "/Identity/Account/Login";
        }
        var response = await fetch('/Carts/AddItem?stockId=' + stockId);
        if (response.status == 200) {
            var result = await response.json();
            var cartCount = document.getElementById("cartCount");
            cartCount.innerHTML = result;
            window.location.href = "#cartCount";
        }
    }
    catch (error) {
        console.log(error);
    }
}