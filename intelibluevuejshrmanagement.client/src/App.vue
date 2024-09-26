<script setup lang="ts">
	import EmployeeForm from './components/EmployeeForm.vue'
	import EmployeeList from './components/EmployeeList.vue'

</script>


<template>
	<main>
		<div class="flex m-auto justify-center mb-5 flex-col">
			<h1 class="text-lg p-3">New Employee</h1>
			<EmployeeForm />
			<hr class="my-5" />
			<h1 class="text-lg p-3">Existing Employee(s)</h1>

			<EmployeeList />
		</div>
		
	</main>
</template>

<style scoped>
</style>
