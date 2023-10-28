const body = document.querySelector("body"),
    modeToggle = body.querySelector(".mode_toggle"),
    sidebar = body.querySelector("nav"),
    sidebarToggle = body.querySelector(".sidebar_toggle");

modeToggle.addEventListener("click", () => {
    body.classList.toggle("dark")
});

sidebarToggle.addEventListener("click", () => {
    sidebar.classList.toggle("close");
});