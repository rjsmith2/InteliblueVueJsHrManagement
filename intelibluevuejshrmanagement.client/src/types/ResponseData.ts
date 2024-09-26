export default interface ResponseData {
	data: any;
	isSuccess: boolean;
	error: { message: string | undefined }
}