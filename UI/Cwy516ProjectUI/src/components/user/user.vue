<template>
    <div>
        <el-dialog title="用户" :visible.sync="formVisible" >
            <el-form ref="form" :model="form">
                <el-form-item label="用户名" prop="userName">
                    <el-input v-model="form.userName" placeholder="用户名"></el-input>
                </el-form-item>
                <el-form-item label="用户编号" prop="userCode">
                    <el-input v-model="form.userCode" placeholder="用户编号"></el-input>
                </el-form-item>
                <el-form-item label="密码" prop="password">
                    <el-input v-model="form.password" placeholder="密码"></el-input>
                </el-form-item>
                <el-form-item>
                    <el-button type="primary" @click="Save">保存</el-button>
                    <el-button type="primary" @click="Cancel">取消</el-button>
                </el-form-item>
            </el-form>
        </el-dialog>
    </div>
</template>

<script>

export default{
    name:"user",
    data(){
        return {
            form:{
                userName:"",
                userCode:"",
                password:""
            },
            formVisible:false
        }
    },
    methods:{
        Save(){
            this.$request.post('home/AddUser',this.form).then(res=>{
                if(res.code==0){
                    this.formVisible=false;
                    this.$emit('ShowAllUser');
                }
                else{
                    Message({
                        type:'success',
                        message:JSON.stringify(res.messages)
                    });
                }
            })
        },
        Cancel(){
            this.formVisible = false;
        },
        Show(){
            this.formVisible = true;
        }
    }
}
</script>
