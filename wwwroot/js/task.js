let addBtn = document.querySelector("#add-btn");
let cover = document.querySelector("#bg__cover");
let addModal = document.querySelector("#modalAdd");
let updateModal = document.querySelector("#modalUpdate");
let deleteModal = document.querySelector("#modalDelete");

var currentDate = new Date().toISOString().split('T')[0];
document.getElementById('duedate').min = currentDate;
document.getElementById('duedateUpdate').min = currentDate;

addBtn.addEventListener("click", function () {
    console.log(cover);
    cover.classList.add("bg__cover");
    addModal.classList.add("openModal");
})

document.addEventListener("click", function (event) {
    if (event.target.classList.contains("btn-close")) {
        cover.classList.remove("bg__cover");
        addModal.classList.remove("openModal")
        updateModal.classList.remove("openModal")
        deleteModal.classList.remove("activeDelete")
    }
});


function deleteForm(id) {
    const form = document.querySelector('#modalDelete');
    cover.classList.add("bg__cover");
    document.getElementById('deleteId').value = id;
    form.classList.add('activeDelete');
}
