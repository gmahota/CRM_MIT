interface JQuery {
    fadeIn(): JQuery;
    fadeOut(): JQuery;
    focus(): JQuery;
    html(): string;
    html(val: string): JQuery;
    show(): JQuery;
    addClass(className: string): JQuery;
    removeClass(className: string): JQuery;
    append(el: HTMLElement): JQuery;
    val(): string;
    val(value: string): JQuery;
    attr(attrName: string): string;
}