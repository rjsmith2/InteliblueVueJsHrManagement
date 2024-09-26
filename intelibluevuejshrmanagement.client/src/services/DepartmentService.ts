import type ResponseData from "@/types/ResponseData";
import type ResponseErrorData from "@/types/ResponseErrorData";

class DepartmentService {
	private endpointUrl: string = `${import.meta.env.VITE_BACKEND_API_URL}/Departments`;

	private async handleResponse<T>(response: Response): Promise<T> {
		if (!response.ok) {
			const errorData: ResponseErrorData = await response.json();
			throw errorData;
		}
		return response.json();
	}

	private handleError(e: ResponseErrorData): never {
		console.error(e.errors || e.error);
		throw e;
	}

	private requestOptions(method: string, data?: any): RequestInit {
		return {
			method,
			headers: { 'Content-Type': 'application/json' },
			body: data ? JSON.stringify(data) : undefined,
		};
	}

	getAll(): Promise<ResponseData | ResponseErrorData | any> {
		return fetch(this.endpointUrl)
			.then(this.handleResponse)
			.catch(this.handleError);
	}

	get(id: any): Promise<ResponseData | ResponseErrorData | any> {
		return fetch(`${this.endpointUrl}/${id}`)
			.then(this.handleResponse)
			.catch(this.handleError);
	}

	create(data: any): Promise<ResponseData | ResponseErrorData | any> {
		return Promise.reject("Not supported");
	}

	update(id: any, data: any): Promise<ResponseData | ResponseErrorData | any> {
		return Promise.reject("Not supported");
	}

	delete(id: any): Promise<ResponseData | ResponseErrorData | any> {
		return Promise.reject("Not supported");
	}
}

export default new DepartmentService();
