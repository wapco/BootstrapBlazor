import Data from "../../modules/data.js"

export function init(id, options) {
    options = {
        ...{
            viewMode: 'DateTime',
            startValue: 0,
            requestId: null,
            onCompleted: null
        },
        ...options
    }
    const el = document.getElementById(id);
    if (el === null) {
        return;
    }

    const listHour = el.querySelector('.bb-flip-clock-list.hour');
    const listMinute = el.querySelector('.bb-flip-clock-list.minute');
    const listSecond = el.querySelector('.bb-flip-clock-list.second');
    const countDown = options.viewMode === "CountDown";

    let startTimestamp = Date.now(); // 起始时间（毫秒）
    const getDate = () => {
        const now = Date.now();
        const elapsed = now - startTimestamp;

        if (options.viewMode === "Count") {
            const totalMs = options.startValue + elapsed;
            const totalSeconds = Math.floor(totalMs / 1000);
            const hours = Math.floor(totalSeconds / 3600);
            const minutes = Math.floor((totalSeconds % 3600) / 60);
            const seconds = totalSeconds % 60;
            return { hours, minutes, seconds };
        }

        if (countDown) {
            if (options.startValue === 0) {
                return { hours: 0, minutes: 0, seconds: 0 };
            }

            const remaining = options.startValue - elapsed;
            if (remaining <= 0) {
                return { hours: 0, minutes: 0, seconds: 0 };
            }

            const totalSeconds = Math.floor(remaining / 1000);
            const hours = Math.floor(totalSeconds / 3600);
            const minutes = Math.floor((totalSeconds % 3600) / 60);
            const seconds = totalSeconds % 60;
            return { hours, minutes, seconds };
        }

        // viewMode: 'DateTime'
        const date = new Date();
        return {
            hours: date.getHours(),
            minutes: date.getMinutes(),
            seconds: date.getSeconds()
        };
    };

    let lastHour;
    let lastMinute;
    let lastSecond;
    const go = () => {
        const { hours, minutes, seconds } = getDate();

        if (lastSecond !== seconds) {
            lastSecond = seconds;
            setTime(listSecond, seconds, countDown);
        }
        if (lastMinute !== minutes) {
            lastMinute = minutes;
            setTime(listMinute, minutes, countDown);
        }
        if (lastHour !== hours) {
            lastHour = hours;
            setTime(listHour, hours, countDown);
        }
        return { hours, minutes, seconds }
    }

    let start = void 0
    let current;
    const flip = ts => {
        if (start === void 0) {
            start = ts;
            current = go();
        }
        const elapsed = ts - start;
        if (elapsed >= 1000) {
            start = ts;
            current = go();
        }

        if (countDown && current.hours === 0 && current.minutes === 0 && current.seconds === 0) {
            options.invoke.invokeMethodAsync(options.onCompleted);
            return;
        }
        options.requestId = requestAnimationFrame(flip);
    }

    options.requestId = requestAnimationFrame(flip);

    Data.set(id, { el, options });
}

export function dispose(id) {
    const clock = Data.get(id)
    if (clock) {
        if (clock.options.requestId) {
            cancelAnimationFrame(clock.options.requestId);
            clock.options.requestId = null;
        }
    }
}

const setTime = (list, time, countDown) => {
    if (list) {
        const leftIndex = Math.floor(time / 10);
        const rightIndex = time % 10;
        const leftFlip = list.children[0];
        const rightFlip = list.children[1];

        list.classList.remove('flip');
        setFlip(leftFlip, leftIndex, countDown);
        setFlip(rightFlip, rightIndex, countDown);
        list.classList.add('flip');
    }
}

const setFlip = (flip, index, countDown) => {
    const before = flip.querySelector('.before');
    if (before) {
        before.classList.remove('before');
    }
    const active = flip.querySelector('.active');
    if (active) {
        active.classList.remove('active');
    }

    const items = flip.children;
    items[index].classList.add('active');
    if (countDown) {
        index++;
        if (index >= items.length) {
            index = 0;
        }
    }
    else {
        index--;
        if (index < 0) {
            index += items.length;
        }
    }
    items[index].classList.add('before');
}
