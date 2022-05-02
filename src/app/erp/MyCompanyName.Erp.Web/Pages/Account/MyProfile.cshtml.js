$V.Setup(({ ref, reactive }) => {
    const layout = {
        labelCol: { span: 4 },
        wrapperCol: { span: 18 },
    };
    const profileForm = reactive({
        username: 'admin',
    });
    const upload = reactive({
        url: `/file/image/avatar/${abp.currentUser.id}`,
        //data: { type: "avatar", name: abp.currentUser.id },
        action: $upload.SaveAvatarUrl
    });
    const avatar = ref(upload.url);
    const onUploadChange = (file) => {
        if (file.status == "done") {
            avatar.value = `${upload.url}?r=${uuid(4)}`;
        }
    };
    return {
        layout,
        profileForm,
        avatar,
        upload,
        onUploadChange,
    };
}, "#my-profile");

