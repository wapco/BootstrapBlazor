import {debounce} from "../../modules/utility.js"
import EventHandler from "../../modules/event-handler.js"

export function init(component, elem, changeOnEnter) {
    const onSearchValueChange = debounce(v => component.invokeMethodAsync("SearchValueChanged", v));
    let isComposing = false;

    EventHandler.on(elem, 'input', '.search-text', e => {
        if (isComposing) {
            return;
        }
        if (changeOnEnter && e.key !== 'Enter') {
            return;
        }
        onSearchValueChange(e.delegateTarget.value);
    });

    EventHandler.on(elem, 'compositionstart', '.search-text', e => {
        isComposing = true;
    });

    EventHandler.on(elem, 'compositionend', '.search-text', e => {
        isComposing = false;
        onSearchValueChange(e.delegateTarget.value);
    });

    EventHandler.on(elem, 'click', '.search-clear', e => {
        component.invokeMethodAsync("SearchValueChanged", "");
        e.preventDefault();
        e.stopPropagation();
    })
}
