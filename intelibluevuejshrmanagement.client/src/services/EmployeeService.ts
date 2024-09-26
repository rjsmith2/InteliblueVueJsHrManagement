import type ResponseData from "@/types/ResponseData";
import type ResponseErrorData from "@/types/ResponseErrorData";

class EmployeeService {
	private endpointUrl: string = `${import.meta.env.VITE_BACKEND_API_URL}/Employees`;

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
		return fetch(this.endpointUrl, this.requestOptions('POST', data))
			.then(this.handleResponse)
			.catch(this.handleError);
	}

	update(id: number, data: any): Promise<ResponseData | ResponseErrorData | any> {
		return fetch(`${this.endpointUrl}/${id}`, this.requestOptions('PUT', data))
			.then(this.handleResponse)
			.catch(this.handleError);
	}

	delete(id: number): Promise<ResponseData | ResponseErrorData | any> {
		return fetch(`${this.endpointUrl}/${id}`, this.requestOptions('DELETE'))
			.then(this.handleResponse)
			.catch(this.handleError);
	}
}

export default new EmployeeService();
