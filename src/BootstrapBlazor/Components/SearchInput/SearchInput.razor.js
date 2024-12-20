let timeoutId;
export function onValueChanged(component, value) {
    clearTimeout(timeoutId);
    timeoutId = setTimeout(async () => {
        await component.invokeMethodAsync('SearchValueChanged', value);
    }, 500);
}
