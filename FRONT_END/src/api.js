import axios from 'axios'

export const BASE_URL = 'https://localhost:44373/api/';
export const END_POINTS = {
	AUTH_LOGIN: 'auth/login',
	AUTH_REGISTER: 'auth/register/',
	USER_MEALS: "meals/mymeals",
	ADD_MEAL: "meals/addmeal",
	DELETE_MEAL: "meals/deletemeal/",
	UPDATE_MEAL: "meals/editmeal/",
};

export const authAxois = axios.create({
	BASE_URL: BASE_URL,
	headers: {
		Authorization: 'Bearer ' + localStorage.getItem('L_T'),
		'Access-Control-Allow-Origin': '*',
		'Access-Control-Allow-Methods': 'GET,PUT,POST,DELETE,PATCH,OPTIONS',
		'Access-Control-Allow-Headers': 'Content-Type',
	},
});

export const createApiEndPoints = (endPoint) => {
	let url = BASE_URL + endPoint;
	return {
		fetch: () => authAxois.get(url),
		create: (newRecord) => authAxois.post(url, newRecord),
		delete: () => authAxois.delete(url),
		update: (editedRecord) => authAxois.put(url, editedRecord),
	};
};