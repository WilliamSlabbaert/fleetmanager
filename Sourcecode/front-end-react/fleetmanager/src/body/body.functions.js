
export const cardItemDisplay = () => {
    setTimeout(() => {
        let elements = document.querySelectorAll('.cardItem');
        elements.forEach(element => {
            if (element.className.includes("hideItem")) {
                element.style.opacity = 0;
                setTimeout(() => {
                    element.style.display = "none"
                }, 350)
            }
            else {
                element.style.opacity = 0;
                setTimeout(() => {
                    element.style.display = "block"
                }, 350)
                setTimeout(() => {
                    element.style.opacity = 1;
                }, 500)
            }
        });
    }, 300)
}

export const resetCardItemDisplay = () => {
    let elements = document.querySelectorAll('.cardItem');
    elements.forEach(element => {
        element.style.opacity = 0;
        element.style.display = "block"
        element.style.opacity = 1;
    });
}
