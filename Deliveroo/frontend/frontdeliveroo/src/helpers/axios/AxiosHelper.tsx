import axios from 'axios';

let token= sessionStorage.getItem("jwtBearer") || "";
axios.defaults.baseURL = 'https://localhost:44338/';
axios.defaults.headers.common = {'Authorization': `bearer ${token}`};

export default axios;