const allSideMenu = document.querySelectorAll('#sidebar .side-menu.top li a');


allSideMenu.forEach(item=> {
	const li = item.parentElement;

	item.addEventListener('click', function () {
		allSideMenu.forEach(i=> {
			i.parentElement.classList.remove('active');
		})
		li.classList.add('active');
	})
});


// TOGGLE SIDEBAR
const menuBar = document.querySelector('#content nav .bx.bx-menu');
const sidebar = document.getElementById('sidebar');

menuBar.addEventListener('click', function () {
	sidebar.classList.toggle('hide');
})



const switchMode = document.getElementById('switch-mode');

switchMode.addEventListener('change', function () {
	if(this.checked) {
		document.body.classList.add('dark');
	} else {
		document.body.classList.remove('dark');
	}
})

const productMenu = document.getElementById('product_menu');
const tableProduct = document.getElementById('table_product');
const productSell = document.getElementById('product_sell');
const tableSell = document.getElementById('table_sell');

productSell.addEventListener('click', () => {
	tableProduct.style.display = 'none';
	tableSell.style.display = 'table';
})

productMenu.addEventListener('click', () => {
	tableSell.style.display = 'none';
	tableProduct.style.display = 'table';
})


var app = angular.module('ProductApp',[])
app.controller("ProductCtrl", function ($scope, $http) {
	$scope.listDistributor
	$scope.GetDistributor= function () {
        $http({
            method: 'GET',
            // headers: { "Authorization": 'Bearer ' + _user.token },
            data: {
                page: $scope.page,
                pageSize: $scope.pageSize
            },
            url: current_url + '/api/Product/get-all',
        }).then(function (response) {  
            $scope.listDistributor = response.data
            console.log(response);
        }).catch(function (error) {
            console.error('Lỗi :', error);
        });

		// $http({
        //     method: 'POST',
        //     // headers: { "Authorization": 'Bearer ' + _user.token },
        //     data: {
        //         page: $scope.page,
        //         pageSize: $scope.pageSize
        //     },
        //     url: current_url + '/api/Product/create-all',
        // }).then(function (response) {  
        //     $scope.listDistributor = response.data
        //     console.log(response);
        // }).catch(function (error) {
        //     console.error('Lỗi :', error);
        // });
    };   
	$scope.GetDistributor()


})