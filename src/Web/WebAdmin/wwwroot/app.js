
NProgress.configure({
    parent: '.main',
    showSpinner: false
});

function startProgress() {
    NProgress.inc();
}

function stopProgress() {
    NProgress.done();
}

function myFunction() {
    console.log("myFunction>>>")
}