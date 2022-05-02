const UploadSingleComponent = Vue.defineComponent({
    props: ['url', "type", "action", "name", "data",
        "beforeUpload", "showUploadList", "onChange"],
    emits: ['update:url'],
    data() {
        return {
            headers: {},
            fileList: [],
        }
    },
    created() {
        this.name = this.name || "file";
        this.$watch("url", (newVal, oldVal) => {
            if (newVal) {
                var index = newVal.lastIndexOf("\/");
                var str = newVal.substring(index + 1, newVal.length);
                this.fileList = [{
                    uid: "-1",
                    status: 'done',
                    name: str,
                    url: newVal,
                    thumbUrl: newVal,
                }];
            };
        }, {
            immediate: true
        });
    },
    methods: {
        handleChange(info) {
            const file = Vue.reactive(info.file);
            this.$emit('change', file);
            if (file.status === 'done') {
                const tempUrl = `${file.response}`;
                this.$emit('update:url', tempUrl);
            }
            if (file.status === 'removed') {
                this.$emit('update:url', undefined);
            }
        },
    },
    template: `<a-upload 
v-model:file-list="fileList" 
:name="name" 
:multiple="true" 
:action="action"
:data="data"
:headers="headers"
:list-type="type"
:show-upload-list="showUploadList"
:before-upload="beforeUpload"
v-on:change="handleChange">
<slot></slot>
</a-upload>`
});


const $L = abp.localization.getResource('MyCompanyNameErp');
const $V = {
    Init: (obj, selectors) => Vue.createApp(obj || {})
        .use(antd)
        .component('upload-single', UploadSingleComponent)
        .mount(selectors || '#app'),
    Setup: (obj, selectors) => {
        return $V.Init({
            setup: () => {
                if (isFunction(obj)) { obj = obj(Vue); }
                let locale = {}; try { locale = __locale } catch (e) { }
                return Object.assign(obj, { locale });
            }
        }, selectors)
    }
};

const $Modal = window.parent.antd.Modal;
const $Message = window.parent.antd.message;
const $Notification = window.parent.antd.notification;
const $alert = {
    info: (content, title) => { return $Modal.info({ title, content }); },
    success: (content, title) => { return $Modal.success({ title, content }); },
    warn: (content, title) => { return $Modal.warning({ title, content }); },
    error: (content, title) => { return $Modal.error({ title, content }); },
};
const $message = {
    loading: (content, key, duration) => { return $Message.loading({ content, key, duration }); },
    info: (content, key, duration) => { return $Message.info({ content, key, duration }); },
    success: (content, key, duration) => { return $Message.success({ content, key, duration }); },
    warn: (content, key, duration) => { return $Message.warning({ content, key, duration }); },
    error: (content, key, duration) => { return $Message.error({ content, key, duration }); },
};
const $notify = {
    info: (message, description) => { return $Notification.info({ message, description }); },
    success: (message, description) => { return $Notification.success({ message, description }); },
    warn: (message, description) => { return $Notification.warning({ message, description }); },
    error: (message, description) => { return $Notification.error({ message, description }); },
};
$alert.warning = $alert.warn;
$message.warning = $message.warn;
$notify.warning = $notify.warn;
const $confirm = function ({ title, content, icon, onOk, onCancel, okText, cancelText, okType }) {
    if (['exclamation'].indexOf(icon) > -1) {
        const { h } = Vue;
        icon = h('span', { className: `anticon anticon-${icon}-circle`, },
            h('i', { className: `fa fa-${icon}-circle`, })
        );
    }
    return window.parent.antd.Modal.confirm({
        title: title,
        content: content,
        icon: icon,
        okType: okType || "primary",
        okText: okText || $L('Sure'),
        cancelText: cancelText || $L('Cancel'),
        onOk: onOk,
        onCancel: onCancel,
    });
};

function isPromise(obj) {
    return !!obj
        && (typeof obj === 'object' || typeof obj === 'function')
        && typeof obj.then === 'function';
}
function isFunction(obj) {
    return !!obj && (typeof obj === 'function');
}
function windowResizeEvent(callback) {
    window.onresize = function () {
        var target = this;
        if (target.resizeFlag) {
            clearTimeout(target.resizeFlag);
        }

        target.resizeFlag = setTimeout(function () {
            callback();
            target.resizeFlag = null;
        }, 100);
    }
}
String.prototype.format = function (args) {
    var result = this;
    if (arguments.length > 0) {
        if (arguments.length == 1 && typeof (args) == "object") {
            for (var key in args) {
                if (args[key] != undefined) {
                    var reg = new RegExp("({" + key + "})", "g");
                    result = result.replace(reg, args[key]);
                }
            }
        }
        else {
            for (var i = 0; i < arguments.length; i++) {
                if (arguments[i] != undefined) {
                    var reg = new RegExp("({)" + i + "(})", "g");
                    result = result.replace(reg, arguments[i]);
                }
            }
        }
    }
    return result;
}

const uuid = (len, radix) => {
    var chars = '0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz'.split('');
    var uuid = [], i;
    radix = radix || chars.length;
    if (len) {
        for (i = 0; i < len; i++) uuid[i] = chars[0 | Math.random() * radix];
    } else {
        var r;
        uuid[8] = uuid[13] = uuid[18] = uuid[23] = '-';
        uuid[14] = '4';
        for (i = 0; i < 36; i++) {
            if (!uuid[i]) {
                r = 0 | Math.random() * 16;
                uuid[i] = chars[(i == 19) ? (r & 0x3) | 0x8 : r];
            }
        }
    }
    return uuid.join('');
}