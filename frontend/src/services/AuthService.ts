import axios from  "axios"

const API_URL = "http://localhost:5006/auth";

export const register = async (data:any) => {
  try {
    const response = await axios.post(`${API_URL}/register`, data);
    return response.data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      throw error.response?.data || error.message;
    } else {
      throw error;
    }
  }
};

export const login = async (data: any) =>{
    const response = await axios.post(`${API_URL}/login`,data);
    return response.data;
}

export const logout = async () => {
  const response = await axios.post(`${API_URL}/logout`,{},{withCredentials:true});
  localStorage.removeItem("token");
  return response;
}

export const addRole = async (data:any)=> {
  const response = await axios.post(`${API_URL}/role`,data);
  return response.data;
}