const appendEmailSuffix = val => {
    let res; let emailSuffix = ['163.com', 'qq.com', '126.com', 'gmail.com', 'outlook.com'];
    if (!val || val.indexOf('@') >= 0) { res = []; }
    else { res = emailSuffix.map(domain => `${val}@${domain}`); }
    return res;
};