export default interface ResponseErrorData extends Error {
	title: any;
	status: boolean;
	errors: {};
	error: string;
}