let updateBtn = document.querySelectorAll("#btn-warning");
let addBtn = document.querySelector("#btn-add");
let addBox = document.querySelector("#modalAdd");
let deleteBtn = document.querySelectorAll("#btn-danger");
let updateBox = document.querySelectorAll("#modalUpdate");
let deleteBox = document.querySelectorAll("#modalDelete");
let btnClose = document.querySelectorAll(".btn-close")
let numInput = document.querySelectorAll(".numInput")
deleteBtn.forEach((btn, index) => {
    btn.addEventListener("click", function () {
        deleteBox[index].classList.add("openModal");
    });
});
updateBtn.forEach((btn, index) => {
    btn.addEventListener("click", function () {
        updateBox[index].classList.add("openModal2");
    });
});
addBtn.addEventListener("click", function () {
    addBox.classList.add("openModal3");
})

document.addEventListener("click", function (event) {
    if (event.target.classList.contains("btn-close")) {
        console.log("sd")
        deleteBox.forEach((box) => {
            box.classList.remove("openModal");
        });
        updateBox.forEach((box) => {
            box.classList.remove("openModal2");
        });
        addBox.classList.remove("openModal3")
    }
});