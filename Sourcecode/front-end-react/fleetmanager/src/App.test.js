import { render, screen } from '@testing-library/react';
import App from './App';
import Body from './body/body'
test('renders title', () => {
  const {getByText} = render(<App />);
  const element = getByText(/Fleetmanager/);
  expect(element).toHaveTextContent("Fleetmanager")
});
test('data fetch test',()=>{
  const {getByLabelText} = render(<Body />);
})